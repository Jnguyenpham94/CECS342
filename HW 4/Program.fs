open System

type AccountStatus = 
    | Empty of int //Empty 0
    | Balance of int //positive balance
    | OverDrawn of int //negative balance

type BankAccount = {name: string; account: AccountStatus; creditLimit: int option}

//take 1 BankAccount 
//RETURNS true if account Balance > 100,000
//PREDICATE
let isWealthy bankAccount =
    match bankAccount.account with
    |Balance e -> if e > 100000 then
                    true
                  else
                    false
    | _ -> false

//list of BankAccounts 
//RETURNS new list containing only accounts that are OverDrawn
//HINT: List.filter
//PREDICATE
let findOverdrawn bankAccount =
    ()

//2 BankAccounts
//RETURNS whichever has larger $. OverDrawn : negative $ Empty: 0
//HINT: write helper to convert account to int
//PREDICATE
let largerAmount a b =
    ()

//list of BankAccounts
//RETURNS new list containing $ 
//HINT: List.map
let accountAmounts bankAccounts =
    ()

//predicate of tpe (BankAccount->bool), list of BankAccounts
//RETURNS new list of $ amounts of accounts that satisfy(true) the predicate
let amountsWhere predicate bankAccounts =
    ()

//list of AccountStatus
//RETURNS sum of $ amounts: Empty is 0; OverDrawn with abs of $; Balance other
let combineAccounts accountStatus =
    ()

//list of BankAccounts
//RETURNS largest $ BankAccount obj; if 2 have same amount RETURNS first one
let wealthiestAccount bankAccount =
    ()

[<EntryPoint>]
let main argv =
    let neal = {name = "Neal Terrell"; account = Balance 100; creditLimit = None}
    let dave = {name = "Dave Davidson"; account = OverDrawn 200; creditLimit = None}
    let tom = {name = "Tom Thompson"; account = Balance 200000; creditLimit = Some 500}
    let jackie = {name = "Jackie Jackson"; account = Empty 0; creditLimit = None}

    isWealthy neal |> printfn "%O" //false
    isWealthy tom |> printfn "%O" //true

    0