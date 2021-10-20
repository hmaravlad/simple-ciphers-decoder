import { IReader } from 'src/types/reader';

export class AsciiReader implements IReader {
  read(text: string): number[] {
    return [...Buffer.from(text, 'ascii')];
  }
}