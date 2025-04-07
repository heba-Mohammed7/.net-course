import { Movie } from './types.js';

export class MovieService {
  private apiKey = '21d6601622ce880a80939f3c1823ce8e';
  private baseUrl = 'https://api.themoviedb.org/3/search/movie';

  async fetchMovies(query: string): Promise<Movie[]> {
    const response = await fetch(`${this.baseUrl}?api_key=${this.apiKey}&query=${query}`);
    const data = await response.json();
    return data.results as Movie[];
  }
}
