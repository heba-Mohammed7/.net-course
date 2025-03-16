const images=document.querySelectorAll("img");
console.log(images);


images.forEach(img => {
    if(img.hasAttribute("alt")){
        img.setAttribute("alt","Old")
    }
    else{
        img.setAttribute("alt","Elzero New")
    }
});


