(*
The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
Find the sum of all the primes below two million.
ANSWER SHOULD BE: 142,913,828,922
*)

//n is value to check if prime
let isPrime n =
    //boolean to stop looping when divisor is found or just to stop while loop for special cases
    let mutable stop = false
    let mutable found = false //if prime F otherwise NOT T
    
    if n % 2 = 0 then
        //printfn "%d is even so NOT PRIME" x
        false
    elif n <= 1 then
        //printfn "%d is NOT PRIME" x
        false
    else
        let mutable i = 3 //all even numbers are prime so start at first odd int after
        while float i <= sqrt (float n) && stop <> true do
            //check for largest int that can be divisor of x
            //if greater STOP
            if n % i = 0 then
                found <- true
                stop <- true
            else
                i <- i + 2 // iterate 2 values to skip even values
        
        //print statements for result of number
        if found = true then
            //printfn "%d is NOT PRIME" x
            false
        else
            //printfn "%d is PRIME" x
            true


//max and return
let sumPrimes max =
    let mutable i = 3
    let mutable sum = 2
    while i <= max do 
        if isPrime i = true then
            sum <- sum + i
        i <- i + 2
    sum


[<EntryPoint>]
let main args =
    let max = 2000000
    let sum = sumPrimes max
    printfn "The sum of primes up to %d is: %d" max sum
    0