function processData(input) {
    input.sort((a, b) => b - a);
    var firstNumber = input[0];
    var secondNumber = 0;
    if (firstNumber % 2 == 1)
    {
      //locate next even
      for(const item of input)
      {
        if(item % 2 == 0) 
        {
          secondNumber = item;
          break;
        }
      }
    }
    else
    {
      //locate next odd
      for(const item of input)
      {
        if(item % 2 == 1) 
        {
          secondNumber = item;
          break;
        }
      }
    }

    return firstNumber + secondNumber;
}

console.log(processData([19, 2, 42, 18]));
console.log(processData([61, 32, 51]));