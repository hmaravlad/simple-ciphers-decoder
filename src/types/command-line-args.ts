import { ArgumentConfig } from 'ts-command-line-args';

export interface IArgs {
  source: string,
  output: string,
  cipher: string,
  outputFormat: string,
}

export const argsConfig: ArgumentConfig<IArgs> = {
  source: String,
  output: String,
  cipher: String,
  outputFormat: String,
};