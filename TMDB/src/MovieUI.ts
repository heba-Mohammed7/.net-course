import { Movie } from './types.js';

export class MovieUI {
  private imageUrl = 'https://image.tmdb.org/t/p/w220_and_h330_face';
  private movieTitle = document.getElementById('movie-title')!;
  private voteAverage = document.getElementById('vote_average')!;
  private popularity = document.getElementById('popularity')!;
  private releaseDate = document.getElementById('release_date')!;
  private description = document.getElementById('movie-description')!;
  private moviesList = document.getElementById('movies-list')!;

  showMoviesList(movies: Movie[], activeIndex: number, onClick: (index: number) => void) {
    this.moviesList.innerHTML = '';
    movies.forEach((movie, index) => {
      const img = document.createElement('img');
      img.src = `${this.imageUrl}${movie.poster_path}`;
      img.alt = movie.title;
      if (index === activeIndex) {
        img.classList.add('active');
      }
      img.onclick = () => onClick(index);
      this.moviesList.appendChild(img);
    });
  }

  showMovieDetails(movie: Movie) {
    this.movieTitle.textContent = movie.title;
    this.voteAverage.textContent = movie.vote_average.toString();
    this.popularity.textContent = `(${movie.popularity})`;
    this.releaseDate.textContent = movie.release_date;

    if (movie.overview.length > 300) {
      const shortText = movie.overview.slice(0, 300);
      this.description.innerHTML = `
        ${shortText}<span id="dots">...</span><span id="more" style="display:none;font-size: 1rem;">${movie.overview.slice(300)}</span>
        <button id="toggle-btn" style="background:none; border:none; color:#F5C51C; cursor:pointer;">See More</button>
      `;
      this.setupToggle();
    } else {
      this.description.textContent = movie.overview;
    }

    document.body.style.backgroundImage = `
      linear-gradient(to right, rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0)),
      url(${this.imageUrl}${movie.backdrop_path})`;
  }

  private setupToggle() {
    const toggleBtn = document.getElementById("toggle-btn");
    const moreText = document.getElementById("more");
    const dots = document.getElementById("dots");

    if (toggleBtn && moreText && dots) {
      toggleBtn.onclick = () => {
        const isHidden = moreText.style.display === "none";
        moreText.style.display = isHidden ? "inline" : "none";
        dots.style.display = isHidden ? "none" : "inline";
        toggleBtn.textContent = isHidden ? "See Less" : "See More";
      };
    }
  }
}
