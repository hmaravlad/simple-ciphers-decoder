const VALID_STRING_REGEX = new RegExp(/^["“”''‘’«‎»‎:/.,;:%\][?!>=+-—\\`\t\s~<@#$%^&*\-_)( )a-zA-Z0-9]+$/g);

export class Filter {
  filterResults(results: string[]): string[] {
    return results.filter((str) => {
      return str
        .split(/\s+/g)
        .reduce((prev, curr) => prev && (curr.match(VALID_STRING_REGEX) !== null), this.returnTrue());
    });
  }

  private returnTrue(): boolean {
    return true;
  }
}