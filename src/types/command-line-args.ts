import { ArgumentConfig } from 'ts-command-line-args';

export interface IArgs {
  source: string,
  output: string,
  cipher: string,
  sourceFormat: string,
}

export const argsConfig: ArgumentConfig<IArgs> = {
  source: String,
  output: String,
  cipher: String,
  sourceFormat: String,
};