open System
//program to check if inputted int is prime

printf "Input a value to check if prime: "
let x = Console.ReadLine() |> int
//boolean to stop looping when divisor is found
let found = false

