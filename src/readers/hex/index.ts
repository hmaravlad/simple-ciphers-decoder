import { IReader } from 'src/types/reader';

export class HexReader implements IReader {
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  read(text: string): number[] {
    throw new Error('NOT IMPLEMENTED');
  }
}