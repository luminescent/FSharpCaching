namespace FSharpCaching 

open CacheLT
open Cache
open Cache2
open CompExprCache

module Main = 


    let getOrAddAndPrint (getOrAddFunc : ('key -> 'value) -> ('key -> 'value)) generator (keys: 'key list) =

        let myGetOrAdd = getOrAddFunc generator

        let results = 
            keys
            |> List.map myGetOrAdd
        printfn "%A" results 
        printfn "%A" (myGetOrAdd keys.Head)


    [<EntryPoint>]
    let main argv = 
        



        let generator i = 
            printfn "In generator for %A" i
            i + 10 

        let cacheCE = new CompExprCache<int, int>(generator)

        let all = 
            [0..10] 
            |> List.map (fun x ->  cacheCE { return! 1 })

//        let all = 
//            [0..10] 
//                |> List.map (fun x ->  cacheCE { return! x })
//


        getOrAddAndPrint getOrAdd generator [0..10]
        getOrAddAndPrint getOrAdd2 generator [0..10]


        getOrAddAndPrint get generator [0..10]




        0 // return an integer exit code
