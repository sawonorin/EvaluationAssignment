function processData(input) {
    var output = "";
    var currentChar = input[0];
    var currentCount = 0;
    for (const val in input)
    {
      if (currentChar == input[val])
      {
        currentCount++;   
        if (val == input.length -1)    
        {
          output = output + input[val].repeat(currentCount < 3 ? currentCount : 3);
        }
      }
      else
      {
        output = output+ currentChar.repeat(currentCount < 3 ? currentCount : 3);
        currentCount = 1;
        currentChar = input[val];
      }
    }

    return output;
}

console.log(processData("ffdttttyy"));
console.log(processData("iiikigggg"));