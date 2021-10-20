export interface IReader {
  read: (text: string) => Uint8Array
}