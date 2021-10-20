import { parse } from 'ts-command-line-args';
import { ReaderFactory } from './readers/reader-factory';
import { IArgs, argsConfig } from './types/command-line-args';
import { readFile } from 'fs/promises';


(async () => {
  const args = parse<IArgs>(argsConfig);
  const ciphertext = (await readFile(args.source)).toString();
  const readerFactory = new ReaderFactory();
  const reader = readerFactory.getReader(args.sourceFormat);
  const ciphertextBinary = reader.read(ciphertext);
  console.dir({ ciphertextBinary });
})();
