import { IReader } from 'src/types/reader';

export class Base64Reader implements IReader {
  read(text: string): number[] {
    return [...Buffer.from(text, 'base64')];
  }
}