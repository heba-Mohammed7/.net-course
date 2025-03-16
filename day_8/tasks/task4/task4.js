
/*-------------------------------------------- task 4 ------------------------------------------*/

const input4 =document.querySelectorAll(".input");
const form=document.getElementById("create");
const res_create =document.querySelector(".results");;
const submit = document.querySelector('input[type="submit"]');
console.log(input4);

document.querySelectorAll('.input').forEach(input =>{
    input.style="width: 50%; display:block; background-color: #eee;margin-bottom:20px;padding:10px 0; border-radius: 10px; border-color: #dfdfdf;";
});
submit.style="width: 50%; display:block; background-color: seagreen;margin-bottom:20px;padding:10px 0; border:none;border-radius: 10px;font-family: sans-serif; color:white;font-weight:bold;";


form.style="width: 90%; margin:20px auto; justify-items: center;"

form.addEventListener("submit", function (x) {
    x.preventDefault();
    res_create.innerHTML = "";
    res_create.style="width:90%; display: grid; gap:20px; grid-template-columns: auto auto auto;text-align: center;";
    
    const num = input4[0].value;
    const text = input4[1].value;
    const type = input4[2].value;
    
    console.log(num);
    console.log(text);
    console.log(type);
    for(let i = 0; i < num; i++){
        const newDiv=document.createElement(type);
        newDiv.setAttribute("class","box");
        newDiv.style="display:inline-block; background-color: #ff5722; padding:5px 10%;border-radius: 10px;padding:10px 0; font-family: sans-serif; color:white;"
        newDiv.textContent =text;
        newDiv.setAttribute("id",`id-${i}`);
        res_create.appendChild(newDiv);
    }

    
});


