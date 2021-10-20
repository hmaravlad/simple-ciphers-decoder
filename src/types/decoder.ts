export interface IDecoder {
  decrypt: (ciphertext: number[]) => string
}