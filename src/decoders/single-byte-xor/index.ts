import { IDecoder } from 'src/types/decoder';
import { Filter } from '../utils/filter';
const DELIMITER = '\n\n===============================================\n\n';

export class SingleByteXorDecoder implements IDecoder {
  decrypt(ciphertext: number[]): string {
    const results = [];
    for (let i = 0; i < 256; i++) {
      results[i] = Buffer.from(ciphertext.map(byte => byte ^ i)).toString('utf8');
      console.dir({ len1: ciphertext.length, len2: results[i].length });
    }
    const filter = new Filter();
    return filter.filterResults(results).join(DELIMITER);
  }
}
