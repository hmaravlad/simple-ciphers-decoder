import { letterFrequencies } from './letter-frequencies';

const MAX_COUNT = 10;

export class FrequencyAnalyzer {
  decrypt(ciphertext: number[]): number[][] {
    const results = [];
    const frequencies: { [k: string]: number }[] = [];
    for (let i = 0; i < 256; i++) {
      results.push(ciphertext.map(byte => byte ^ i));
      const charSum = results[i].reduce((prev, curr) => prev + (this.isLetter(curr) ? 1 : 0), 0);
      frequencies.push({});
      results[i].map(charCode => {
        if (this.isLetter(charCode)) {
          const char = String.fromCharCode(charCode);
          if (frequencies[i][char]) {
            frequencies[i][char] += 1;
          } else {
            frequencies[i][char] = 1;
          }
        }
      });
      for (const key in frequencies[i]) {
        frequencies[i][key] /= charSum;
      }
    }
    const change = this.countChange(frequencies);
    return results
      .map((res, i) => ({ res, i }))
      .sort(({ i: i1 }, { i: i2 }) => {
        return change[i1] - change[i2];
      })
      .map(({ res }) => res)
      .slice(0, MAX_COUNT);
  }

  countChange(frequencies: { [k: string]: number }[]): number[] {
    const res = [];
    for (let i = 0; i < frequencies.length; i++) {
      res.push(0);
      for (const key in letterFrequencies) {
        res[i] += Math.abs(letterFrequencies[key] - ((frequencies[i][key] || 0) + (frequencies[i][key.toUpperCase()] || 0)));
      }
    }
    return res;
  }

  isLetter(char: number): boolean {
    return (char >= 65 && char <= 90) || (char >= 97 && char <= 122);
  }

  returnFalse(): boolean {
    return false;
  }
}