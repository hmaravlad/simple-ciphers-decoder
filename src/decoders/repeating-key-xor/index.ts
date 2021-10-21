import { IDecoder } from 'src/types/decoder';

const SHARPNESS_CRITERION = 0.3;

export class RepeatingKeyXorDecoder implements IDecoder {
  decrypt(ciphertext: number[]): string {
    return this.getKeyLength(ciphertext).toString();
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