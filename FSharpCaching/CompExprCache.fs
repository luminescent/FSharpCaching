namespace FSharpCaching 

open System.Collections.Concurrent

module CompExprCache = 

    type CompExprCache<'key, 'value>(valueFactory) = 
        let store = new ConcurrentDictionary<'key, 'value>()

        member this.ReturnFrom(x) =
            if not (store.ContainsKey x) then 
                store.TryAdd(x, valueFactory x) 
                |> ignore

            store.[x]

