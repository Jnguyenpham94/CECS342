
open System

type AccountStatus = 
    | Empty of int
    | Balance of int
    | OverDrawn of int

type BankAccount = {name: string; account: AccountStatus; creditLimit: int option}

//function logic below
//AccountStatus = Empty 0 
let emptyCase bankAccount requestInt (b : int) =
    if bankAccount.creditLimit = None then
        printfn "Got TO emptyCase 1 %O" b
        let x = (requestInt, {name = bankAccount.name; account = OverDrawn (requestInt - b); creditLimit = bankAccount.creditLimit})
        x
        elif bankAccount.creditLimit > Some requestInt then
            printfn "Got TO emptyCase 2 %O" b
            let y = (requestInt, {name = bankAccount.name; account = OverDrawn (b - requestInt); creditLimit = bankAccount.creditLimit})
            y
        else
            printfn "Got TO emptyCase 3 %O" b
            let remains = min bankAccount.creditLimit.Value requestInt //withdraw up to the credit limit
            let z = (remains, {name = bankAccount.name; account = OverDrawn remains; creditLimit = bankAccount.creditLimit})
            z

//AccountStatus = Balance > 0
let balanceCase bankAccount requestInt b =
    if requestInt < b then //balance - request
        printfn "Got TO balanceCase 1 %O" b
        let bal = b - requestInt
        let x = (requestInt, {name = bankAccount.name; account = Balance bal; creditLimit = bankAccount.creditLimit})
        x
    elif b = requestInt then //balance = request
        printfn "Got TO balanceCase 2 %O" b
        let y = (requestInt, {name = bankAccount.name; account = Empty 0; creditLimit = bankAccount.creditLimit})
        y
    else //basically emptycase
        printfn "Got TO balanceCase 3 %O" b
        let z = emptyCase bankAccount requestInt b
        z

//withdraw cases
let withdraw bankAccount requestInt =
    match bankAccount.account with
    | OverDrawn o -> (0, {name = bankAccount.name; account = bankAccount.account; creditLimit = bankAccount.creditLimit})
    | Empty e -> emptyCase bankAccount requestInt e
    | Balance b -> balanceCase bankAccount requestInt b



[<EntryPoint>]
let main argv =
    //let m_burns = {name = "Montgomery Burns"; account = Balance 100000; creditLimit = Some 10000}
    //let bob = {name = "Robert Dugalle"; account = Empty 0; creditLimit = Some 100}
    let neal = {name = "Neal Terrell"; account = Balance 100; creditLimit = None}
    let dave = {name = "Dave Davidson"; account = OverDrawn 200; creditLimit = Some 0}
    let tom = {name = "Tom Thompson"; account = Balance 100; creditLimit = Some 500}
    //let result1 = withdraw m_burns 100
    let result2 = withdraw neal 50
    let result2_1 = withdraw neal 1000
    let result2_2 = withdraw neal 100
    //let result3 = withdraw bob 1000 //Case: credit < request
    let result4 = withdraw dave 300
    let result5 = withdraw tom 1000
    //printfn "%O" result1
    printfn "%O" result2
    printfn "%O" result2_1
    printfn "%O" result2_2
    //printfn "%O" result3
    printfn "%O" result4
    printfn "%O" result5
    0 // return an integer exit code