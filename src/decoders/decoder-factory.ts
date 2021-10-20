import { IDecoder } from 'src/types/decoder';
import { SingleByteXorDecoder } from './single-byte-xor';

export class DecoderFactory {
  private decoders: { [key: string]: IDecoder };

  constructor() {
    this.decoders = {
      'single-byte-xor': new SingleByteXorDecoder(),
    };
  }

  getDecoder(cipher: string): IDecoder {
    const decoder = this.decoders[cipher];
    if (!decoder) throw new Error(`Cipher: ${cipher} is not supported`);
    return decoder;
  }
}