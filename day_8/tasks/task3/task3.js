

/*-------------------------------------------- task 3 ------------------------------------------*/
const input = document.querySelector('input');
const res =document.querySelector(".result");


input.addEventListener('input', function () {
    console.log(input.value);
    res.textContent = `${input.value} USD Dollar = ${(input.value*50.60).toFixed(2) } Egyptian Pound`;
  });


