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
    |Balance b -> if b > 100000 then
                    true
                  else
                    false
    | _ -> false

//list of BankAccounts 
//RETURNS new list containing only accounts that are OverDrawn
//HINT: List.filter
//PREDICATE
let findOverdrawn bankAccount =
    bankAccount |> List.filter (fun a -> match a.account with
                                            | OverDrawn o-> true
                                            | _ -> false)

//Helper largerAmount
//converts account amount to int Empty = 0; Balance = pos; OverDrawn = neg
let convertInt a =
    match a.account with
    |Empty e-> 0
    |Balance b-> b
    |OverDrawn o-> o * (-1)


//2 BankAccounts
//RETURNS whichever has larger $. OverDrawn : negative $; Empty: 0
//HINT: write helper to convert account to int
//PREDICATE
let largerAmount a b =
    let testA = convertInt a
    let testB = convertInt b
    if testA > testB then
        a
    else
        b

//list of BankAccounts
//RETURNS new list containing $ 
//HINT: List.map
let accountAmounts bankAccounts =
    bankAccounts |> List.map (fun a -> convertInt a)

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
    findOverdrawn [neal; dave; tom] |> printfn "%O" //[{name = "Dave Davidson"; account = Overdrawn 200; creditLimit = None}]
    largerAmount neal dave |> printfn "Larger amount:\n%O" //{name = "Neal Terrell"; account = Balance 100; creditLimit = None}
    accountAmounts [neal; dave; tom; jackie] |> printfn "Account amounts:\n%O"
    //amountsWhere isWealthy [neal; dave; tom; jackie] |> printfn "%O"

    0