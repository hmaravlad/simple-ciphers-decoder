import { IDecoder } from 'src/types/decoder';

const VALID_STRING_REGEX = new RegExp(/^["“”'/.,;:\-_)( )a-zA-Z0-9]*$/g);
const DELIMITER = '\n\n===============================================\n\n';

export class SingleByteXorDecoder implements IDecoder {
  decrypt(ciphertext: number[]): string {
    const results = [];
    for (let i = 0; i < 256; i++) {
      results[i] = Buffer.from(ciphertext.map(byte => byte ^ i)).toString('utf8');
    }
    return this.filterResults(results).join(DELIMITER);
  }

  filterResults(results: string[]): string[] {
    return results.filter((str) => {
      return str
        .split('\n')
        .reduce((prev, curr) => prev && (curr.match(VALID_STRING_REGEX) !== null), this.returnTrue());
    });
  }

  returnTrue(): boolean {
    return true;
  }
}
