var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import { MovieService } from './MovieService.js';
import { MovieUI } from './MovieUI.js';
export class MovieApp {
    constructor() {
        this.movieService = new MovieService();
        this.movieUI = new MovieUI();
        this.allMovies = [];
        this.movieIndex = 0;
        this.init();
    }
    init() {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.loadMovies("batman");
            this.setupEventListeners();
        });
    }
    loadMovies(query) {
        return __awaiter(this, void 0, void 0, function* () {
            this.allMovies = yield this.movieService.fetchMovies(query);
            this.movieIndex = 0;
            this.render();
        });
    }
    render() {
        if (this.allMovies.length > 0) {
            this.movieUI.showMoviesList(this.allMovies, this.movieIndex, (index) => this.onMovieClick(index));
            this.movieUI.showMovieDetails(this.allMovies[this.movieIndex]);
        }
    }
    onMovieClick(index) {
        this.movieIndex = index;
        this.render();
    }
    setupEventListeners() {
        var _a, _b;
        (_a = document.querySelector(".next")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", () => {
            if (this.movieIndex < this.allMovies.length - 1) {
                this.movieIndex++;
                this.render();
            }
        });
        (_b = document.querySelector(".previous")) === null || _b === void 0 ? void 0 : _b.addEventListener("click", () => {
            if (this.movieIndex > 0) {
                this.movieIndex--;
                this.render();
            }
        });
        const searchInput = document.getElementById("search-input");
        const searchIcon = document.getElementById("search-icon");
        searchIcon === null || searchIcon === void 0 ? void 0 : searchIcon.addEventListener("click", () => {
            searchInput.classList.toggle("active");
            searchInput.focus();
        });
        searchInput === null || searchInput === void 0 ? void 0 : searchInput.addEventListener("input", () => {
            this.loadMovies(searchInput.value);
        });
    }
}
