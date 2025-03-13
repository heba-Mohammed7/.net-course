/*-------------------------------- task 1 ------------------------------------*/ 

let mix = [1, 2, 3, "E", 4, "l", "z", "e", "r", 5, "o"];
let letters = mix.map((x) => (isNaN(x)?x : ""));
let Concatenate =letters.reduce( (acc,x) => (acc+x))
console.log(Concatenate )

/*-------------------------------- task 2 ------------------------------------*/ 

let myString = "EElllzzzzzzzeroo";
let newString = myString.split("").filter((x, index) => myString.indexOf(x) === index);
console.log(newString.join(""));

/*-------------------------------- task 3 ------------------------------------*/ 

function totalVotes (arr) {
    return arr.reduce(function (acc, current) {
        return ((current.voted) == true? (++acc) : acc)
      },0);
}

var voters = [
    {name: 'Bob', age: 30, voted: true},
    {name: 'Jake' , age: 32, voted: true},
    {name: 'Kate' , age: 25, voted: false}, 
    {name: 'Sam' , age: 20, voted: false},
    {name: 'Phil', age: 54, voted: true},
    {name: 'Tami', age: 21, voted: true}, 
    {name: 'Ed' , age:55, voted: true},
    {name: 'Mary', age: 31, voted: false},
    {name: 'Becky', age: 43, voted: false},
    {name: 'Joey', age: 41, voted: true},
    {name: 'Jeff', age: 30, voted: true},
    {name: 'Zack', age: 19, voted: false}]


console.log(totalVotes(voters)); // 7