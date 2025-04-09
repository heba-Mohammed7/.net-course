export class MovieUI {
    constructor() {
        this.imageUrl = 'https://image.tmdb.org/t/p/original';
        this.movieTitle = document.getElementById('movie-title');
        this.voteAverage = document.getElementById('vote_average');
        this.popularity = document.getElementById('popularity');
        this.releaseDate = document.getElementById('release_date');
        this.description = document.getElementById('movie-description');
        this.moviesList = document.getElementById('movies-list');
    }
    showMoviesList(movies, activeIndex, onClick) {
        this.moviesList.innerHTML = '';
        movies.forEach((movie, index) => {
            const poster = document.createElement('div');
            poster.className = 'poster';
            const img = document.createElement('img');
            img.src = `${this.imageUrl}${movie.poster_path}`;
            img.alt = movie.title;
            if (index === activeIndex) {
                img.classList.add('active');
            }
            img.onclick = () => onClick(index);
            //this.moviesList.appendChild(img);
            poster.appendChild(img);
            this.moviesList.appendChild(poster);
        });
    }
    showMovieDetails(movie) {
        this.movieTitle.textContent = movie.title;
        this.voteAverage.textContent = movie.vote_average.toString();
        this.popularity.textContent = `(${movie.popularity})`;
        this.releaseDate.textContent = movie.release_date;
        if (movie.overview.length > 300) {
            const shortText = movie.overview.slice(0, 300);
            this.description.innerHTML = `
        ${shortText}<span id="dots">...</span><span id="more" style="display:none;">${movie.overview.slice(300)}</span>
        <button id="toggle-btn" style="background:none; border:none; color:#F5C51C; cursor:pointer; font-size: 1rem">See More</button>
      `;
            this.setupToggle();
        }
        else {
            this.description.textContent = movie.overview;
        }
        document.body.style.backgroundImage = `
      linear-gradient(to right, rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0)),
      url(${this.imageUrl}${movie.backdrop_path})`;
    }
    setupToggle() {
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
