namespace System.Windows.Forms.GamePlayerFS

module gameControl = 
    open System.Windows.Forms
    open System.Timers
    open System.Drawing
    open System.Linq
    open System
    open Draw

    type QGInternal<'a> = 
        {
        draw: ('a -> Image.Type); 
        tick: ('a -> 'a); 
        mouse: ('a -> 'a);
        key: ('a -> 'a);
        initial: 'a
        }

    type QuickGame<'a>(initial, ?draw, ?tick, ?mouse, ?key) = 
        member x.contained : QGInternal<'a> = 
            {
                draw = defaultArg draw (fun _ -> Image.empty);
                tick = defaultArg tick id;
                mouse = defaultArg mouse id;
                key = defaultArg key id;
                initial = initial
            }
            
    let startGame<'a> (width:int)(height:int)(tps:int)(game:QuickGame<'a>) = 
        let form = new System.Windows.Forms.Form()
        let tick = new Timer()
        let g = game.contained
        let mutable state = g.initial
        let mutable closed = false

        let draw() =
            if form.InvokeRequired then
                form.Invoke(new Action(form.Refresh)) |> ignore
            else form.Refresh()

        let choose v e = 
            match v with
            | Some v' -> v'
            | None -> e

        tick.Interval <- (1. / double tps) / 1000.
        form.Size <- new Size(width, height)
        tick.Elapsed.Add (fun _ -> (state <- g.tick state) |> draw)
        form.Paint.Add (fun e -> Image.draw e.Graphics (g.draw state))
        form.ClientSize <- new Size(width, height)
        tick.AutoReset <- true
        form.Show()
        tick.Start()
        form.Closed.Add (ignore >> tick.Stop >> tick.Dispose)

        tick.Elapsed |> Observable.map (fun _ -> state)