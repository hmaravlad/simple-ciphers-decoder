import { IReader } from 'src/types/reader';

export class AsciiReader implements IReader {
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  read(text: string): number[] {
    throw new Error('NOT IMPLEMENTED');
  }
}