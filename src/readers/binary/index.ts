import { IReader } from 'src/types/reader';

export class BinaryReader implements IReader {
  read(text: string): number[] {
    if (text.length < 8 || text.length % 8 !== 0) {
      throw new Error('invalid binary text');
    }
    const bytes: number[] = [];
    for (let i = 0; i < text.length; i += 8) {
      console.dir({ text, part: text.slice(i, i + 8) });
      const byte = parseInt(text.slice(i, i + 8), 2);
      console.dir({ byte, byte2: byte.toString(2) });
      bytes.push(byte);
    }
    return bytes;
  }
}