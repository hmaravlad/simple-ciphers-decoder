import { IReader } from 'src/types/reader';
import { AsciiReader } from './ascii';
import { Base64Reader } from './base64';
import { BinaryReader } from './binary';
import { HexReader } from './hex';


export class ReaderFactory {
  private readers: { [key: string]: IReader };

  constructor() {
    this.readers = {
      ascii: new AsciiReader(),
      base64: new Base64Reader(),
      binary: new BinaryReader(),
      hex: new HexReader(),
    };
  }

  getReader(sourceFormat: string): IReader {
    const reader = this.readers[sourceFormat];
    if (!reader) throw new Error(`Source format: ${sourceFormat} is not supported`);
    return reader;
  }
}