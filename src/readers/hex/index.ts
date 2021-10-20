import { IReader } from 'src/types/reader';

export class HexReader implements IReader {
  read(text: string): number[] {
    if (text.length < 2 || text.length % 2 !== 0) {
      throw new Error('invalid hex text');
    }
    const bytes: number[] = [];
    for (let i = 0; i < text.length; i += 2) {
      const byte = parseInt(text.slice(i, i + 2), 16);
      bytes.push(byte);
    }
    return bytes;
  }
}