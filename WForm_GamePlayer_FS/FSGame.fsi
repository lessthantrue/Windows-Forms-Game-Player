namespace System.Windows.Forms.GamePlayerFS

module gameControl =

    type internal QGInternal<'a>

    type public QuickGame<'a> =
        new : initial:'a *
              ?draw:('a -> Draw.Image.Type) *
              ?tick:('a -> 'a) *
              ?mouse:(System.Windows.Forms.MouseEventArgs -> 'a -> 'a) *
              ?key:(System.Windows.Forms.KeyEventArgs -> 'a -> 'a) -> QuickGame<'a>
        member internal contained : QGInternal<'a>

    type internal BufferedForm = 
        class
            inherit System.Windows.Forms.Form
            new : unit -> BufferedForm
        end

    val public startGame<'a> : int -> int -> int -> QuickGame<'a> -> System.IObservable<'a>