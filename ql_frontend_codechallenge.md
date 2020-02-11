```
 __        ___  ___       __             __  
/  \ |  | |__  |__  |\ | /__` |     /\  |__) 
\__X \__/ |___ |___ | \| .__/ |___ /~~\ |__) 
```

**Code Challenge**
============

**Part 1 - Algortithms**

**1.1**

Write a function that finds and removes instances of four identical consecutive lowercase letters. The function should delete as a few letters as possible.

Assume that the maximum length of the string is 150 000 however please ellaborate on changes you would do if the maximum length would be 20 million or higher?

Examples: 
``` 
"ffdttttyy" should return "ffdtttyy"
```

``` 
"iiikigggg" should return "iiikiggg"
```

**1.2**

Write a function that takes an array of numbers and returns the maximum sum of two numbers whose digits have an odd sum.

Assume that the array contains between 1 and 150 000 elements and that each element is within the range of 1 to 1 500 000.

Examples:
```
[19, 2, 42, 18] should return 61
```

```
[61, 32, 51] should return 93
```


**Part 2 - Components**

![](inspiration.gif)

***Inspiration***


Create a credit card form for sumbmitting payments. Focus should be on validation and ease of use.
- Consider what we can validate / detect without making a request of the payment server.
- Include unit / e2e tests
- Bundle your module with webpack or parcel.