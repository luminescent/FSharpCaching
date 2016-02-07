namespace FSharpCaching 

// credits to http://fsharpnews.blogspot.co.uk/2013/10/a-thread-safe-object-cache.html

module Cache2 = 

    let get create =
      let cache = System.Collections.Concurrent.ConcurrentDictionary<'a, 'b>()
      fun id -> cache.GetOrAdd(id, System.Func<_, _>(create))
