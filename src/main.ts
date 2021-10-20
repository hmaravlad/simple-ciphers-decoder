import { parse } from 'ts-command-line-args';
import { ReaderFactory } from './readers/reader-factory';
import { IArgs, argsConfig } from './types/command-line-args';
import { readFile, writeFile } from 'fs/promises';
import { DecoderFactory } from './decoders/decoder-factory';


(async () => {
  const args = parse<IArgs>(argsConfig);
  const ciphertext = (await readFile(args.source)).toString();
  const readerFactory = new ReaderFactory();
  const reader = readerFactory.getReader(args.sourceFormat);
  const ciphertextBytes = reader.read(ciphertext);
  const decoderFactory = new DecoderFactory();
  const decoder = decoderFactory.getDecoder(args.cipher);
  const decoded = decoder.decrypt(ciphertextBytes);
  await writeFile(args.output, decoded);
})();
