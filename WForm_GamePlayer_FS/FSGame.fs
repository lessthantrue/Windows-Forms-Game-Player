namespace System.Windows.Forms.GamePlayerFS

module gameControl = 
    open System.Windows.Forms
    open System.Timers
    open System.Drawing
    open System.Linq
    open System
    open Draw

    [<NoComparison>]
    [<NoEquality>]
    type QGInternal<'a> = 
        {
        draw: ('a -> Image.Type); 
        tick: ('a -> 'a); 
        mouse: (System.Windows.Forms.MouseEventArgs -> 'a -> 'a);
        key: (System.Windows.Forms.KeyEventArgs -> 'a -> 'a);
        initial: 'a
        }

    type public QuickGame<'a>(initial, ?draw, ?tick, ?mouse, ?key) = 
        member x.contained : QGInternal<'a> = 
            {
                draw = defaultArg draw (fun _ -> Image.empty);
                tick = defaultArg tick id;
                mouse = defaultArg mouse (fun _ -> id);
                key = defaultArg key (fun _ -> id);
                initial = initial
            }
            
    type BufferedForm() =
        inherit System.Windows.Forms.Form ()
        do
        base.DoubleBuffered <- true

    let startGame<'a> (width:int)(height:int)(tps:int)(game:QuickGame<'a>) =
    
        let form = new BufferedForm()
        let tick = new Timer()
        let g = game.contained
        let mutable state = g.initial
        let mutable closed = false

        let draw() =
            if form.InvokeRequired then
                form.Invoke(Action form.Refresh) |> ignore
            else form.Refresh()

        tick.Interval <- float ((1.f / single tps) * 1000.f)
        //form.Size <- new Size(width, height)    
        form.ClientSize <- new Size(width, height)
        tick.AutoReset <- true
        
        tick.Elapsed.Add (fun _ -> (state <- g.tick state) |> draw)    
        form.Paint.Add (fun e -> Image.draw e.Graphics (g.draw state))
        form.Closed.Add (ignore >> tick.Stop >> tick.Dispose)
        form.KeyDown.Add (fun w -> (state <- g.key w state) |> draw)
        (Observable.merge form.MouseClick form.MouseMove) |> Observable.add(fun m -> (state <- g.mouse m state) |> draw)
          
        tick.Start()

        Application.Run(form)
        
        tick.Disposed |> Observable.map (fun _ -> state)