/* ----------------------------- task 1 -----------------------------*/
console.log("--------------- task 1 ---------------")
let myInfo = {
    username: "Osama",
    role: "Admin",
    country: "Egypt",
};
const map = new Map(Object.entries(myInfo));
console.log(map);
console.log("\n");


/* ----------------------------- task 2 -----------------------------*/
console.log("--------------- task 2 ---------------");

let theNumber = 100020003000;

const set = Number([...new Set(theNumber.toString())].filter(digit => digit !== '0').join(''));
console.log(set);
console.log("\n");




/* ----------------------------- task 3 -----------------------------*/
console.log("--------------- task 3 ---------------");

let url1 = 'elzero.org';
let url2 = 'http://elzero.org';
let url3 = 'https://elzero.org';
let url4 = 'https://www.elzero.org';
let url5 = 'https://www.elzero.org:8080/articles.php?id=100&cat=topics';

let re = /(https?:\/\/)?(www\.)?([\w]+\.\w+)(:\d+)?(\/.*)?/;

console.log(url1.match(re));
console.log(url2.match(re));
console.log(url3.match(re));
console.log(url4.match(re));
console.log(url5.match(re));
console.log("\n");




/* ----------------------------- task 4 -----------------------------*/
console.log("--------------- task 4 ---------------");

let date1 = "25/10/1982";
let date2 = "25 - 10 - 1982";
let date3 = "25 10 1982";
let date4 = "25 10 82";

let re4 = /(\d{2})\s*[\/-\s]\s*(\d{2})\s*[\/-\s]\s*(\d{2,4})/; 

console.log(date1.match(re4)); 
console.log(date2.match(re4)); 
console.log(date3.match(re4)); 
console.log(date4.match(re4)); 
console.log("\n");



/* ----------------------------- task 5 -----------------------------*/
console.log("--------------- task 5 ---------------");

let chosen = 3;

let myFriends = [
  { title: "Osama", age: 39, available: true, skills: ["HTML", "CSS"] },
  { title: "Ahmed", age: 25, available: false, skills: ["Python", "Django"] },
  { title: "Sayed", age: 33, available: true, skills: ["PHP", "Laravel"] },
];

const { title, age, available, skills: [,secSkill] } = myFriends[chosen - 1];

console.log(title);
console.log(age);
console.log(`${available ? "Available" : "Not Available"}`);
console.log(secSkill);
console.log("\n");



/* ----------------------------- task 6 -----------------------------*/
console.log("--------------- task 6 ---------------");

let chars = ["Z", "Y", "A", "D", "E", 10, 1];

let letters = chars.filter(el => typeof el === "string");
let numCount = chars.filter(el => typeof el === "number").length;
let newChars = [...letters.slice(0, numCount), ...letters];
console.log(newChars);
console.log("\n");



/* ----------------------------- task 7 -----------------------------*/

console.log("--------------- task 7 ---------------");


let arr1 = ["Ahmed", "Sameh", "Sayed"];
let arr2 = ["Mohamed", "Gamal", "Amir"];
let arr3 = ["Haytham", "Shady", "Mahmoud"];


const [, b, c] = arr3;
const a = arr1[0];

console.log(`My Best Friends: ${a}, ${b}, ${c}`);
console.log("\n");



/* ----------------------------- task 8 -----------------------------*/
console.log("--------------- task 8 ---------------");


let myFriends8 = ["Osama", "Ahmed", "Sayed", "Sayed", "Mahmoud", "Osama"];
const friendsSet = new Set(myFriends8.sort());
console.log(friendsSet);
console.log("\n");