import { Movie } from './types.js';
import { MovieService } from './MovieService.js';
import { MovieUI } from './MovieUI.js';

export class MovieApp {
  private movieService = new MovieService();
  private movieUI = new MovieUI();
  private allMovies: Movie[] = [];
  private movieIndex = 0;

  constructor() {
    this.init();
  }

  private async init() {
    await this.loadMovies("batman");
    this.setupEventListeners();
  }

  private async loadMovies(query: string) {
    this.allMovies = await this.movieService.fetchMovies(query);
    this.movieIndex = 0;
    this.render();
  }

  private render() {
    if (this.allMovies.length > 0) {
      this.movieUI.showMoviesList(this.allMovies, this.movieIndex, (index) => this.onMovieClick(index));
      this.movieUI.showMovieDetails(this.allMovies[this.movieIndex]);

      const scroll = document.querySelector('.scroll-list') as HTMLElement;
      const posters = scroll.querySelectorAll('.poster');
      const activePoster = posters[this.movieIndex] as HTMLElement;
      if (scroll && activePoster) {        
        const scrollLeft = activePoster.offsetLeft -scroll.offsetWidth / 2 + activePoster.offsetWidth/ 2;
  
        scroll.scrollTo({ left: scrollLeft, behavior: 'smooth' });
      }
    }
  }

  private onMovieClick(index: number) {
    this.movieIndex = index;
    this.render();
  }

  private setupEventListeners() {

    document.querySelector(".next")?.addEventListener("click", () => {
      if (this.movieIndex < this.allMovies.length - 1) {
        this.movieIndex++;
        
        this.render();
      }
    });

    document.querySelector(".previous")?.addEventListener("click", () => {
      if (this.movieIndex > 0) {
        this.movieIndex--;
        this.render();
      }
    });

    const searchInput = document.getElementById("search-input") as HTMLInputElement;
    const searchIcon = document.getElementById("search-icon");

    searchIcon?.addEventListener("click", () => {
      searchInput.classList.toggle("active");
      searchInput.focus();
    });

    searchInput?.addEventListener("input", () => {
      this.loadMovies(searchInput.value);
    });
  }
}
