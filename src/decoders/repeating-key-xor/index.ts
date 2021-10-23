import { IDecoder } from 'src/types/decoder';
import { Filter } from '../utils/filter';
import { FrequencyAnalyzer } from './frequency-analyzer';

const SHARPNESS_CRITERION = 0.3;

type Str = number[];

function textGroupShuffle(texts: Str[][], keyLength: number, step = 0): Str[][] {
  if (step === keyLength) return [[]];
  let result: Str[][] = [];
  for (let i = 0; i < texts[step].length; i++) {
    result = result.concat(textGroupShuffle(texts, keyLength, step + 1).map(arr => {
      const arr1 = [...arr];
      arr1.push(texts[step][i]);
      return arr1;
    }));
  }
  return result;
}

export class RepeatingKeyXorDecoder implements IDecoder {
  decrypt(ciphertext: number[]): string {
    const keyLength = this.getKeyLength(ciphertext);
    const frequencyAnalyzer = new FrequencyAnalyzer();
    const ciphertexts: number[][] = new Array(keyLength).fill(0).map(() => []);
    ciphertext.map((char, i) => {
      ciphertexts[i % keyLength].push(char);
    });
    const textGroups = textGroupShuffle(ciphertexts.map(ctext => {
      return frequencyAnalyzer.decrypt(ctext);
    }), keyLength).map(arr => arr.reverse());
    const allTexts = [];
    for (let k = 0; k < textGroups.length; k++) {
      const text: number[] = [];
      for (let i = 0; i < textGroups[k][0].length; i++) {
        for (let j = 0; j < keyLength; j++) {
          if (textGroups[k][j][i] != undefined) {
            text.push(textGroups[k][j][i]);
          }
        }
      }
      allTexts.push(text);
    }
    const filter = new Filter();
    const allValidResults = filter.filterResults(allTexts.map(bytes => Buffer.from(bytes).toString('utf8')));
    return allValidResults.join('\n\n======================================================\n\n\n');
  }

  getKeyLength(ciphertext: number[]): number {
    const coincidences = this.getNumberOfCoincidences(ciphertext);
    const possibleKeyLength = [];
    for (let keyLength = 0; keyLength < coincidences.length; keyLength++) {
      let keySum = 0;
      let nonKeySum = 0;
      for (let j = 2; j < coincidences.length - 1; j += 1) {
        if (j % keyLength === 0) {
          keySum += coincidences[j];
        } else {
          nonKeySum += coincidences[j];
        }
      }
      const keyAverage = keySum / (coincidences.length / keyLength);
      const nonKeyAverage = nonKeySum / (coincidences.length - coincidences.length / keyLength);
      if (keyAverage * SHARPNESS_CRITERION > nonKeyAverage) {
        possibleKeyLength.push(keyLength);
      }
    }
    return possibleKeyLength[0];
  }

  getNumberOfCoincidences(ciphertext: number[]): number[] {
    const result = [];
    for (let offset = 0; offset < ciphertext.length; offset++) {
      let counter = 0;
      for (let i = 0; i < ciphertext.length - offset; i++) {
        if (ciphertext[i] === ciphertext[i + offset]) {
          counter += 1;
        }
      }
      result.push(counter);
    }
    return result;
  }
}