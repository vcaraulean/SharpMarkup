namespace SharpMarkup

module Definitions =

  //-- | Style of list numbers.
  type ListNumberStyle = DefaultStyle
                       | Example
                       | Decimal
                       | LowerRoman
                       | UpperRoman
                       | LowerAlpha
                       | UpperAlpha 

  //-- | Delimiter of list numbers.
  type ListNumberDelim = DefaultDelim
                       | Period
                       | OneParen
                       | TwoParens 

  //-- | List attributes.
  type ListAttributes = int * ListNumberStyle * ListNumberDelim

  //-- | Type of quotation marks to use in Quoted inline.
  type QuoteType = 
    | SingleQuote 
    | DoubleQuote
  
  //-- | Attributes: identifier, classes, key-value pairs
  type Attr = string * string list * (string * string) list

  //-- | Type of math element (display or inline).
  type MathType = 
    | DisplayMath 
    | InlineMath
  
  //-- | Formats for raw blocks
  type Format = string

  //-- | Link target (URL, title).
  type Target = string * string

  //-- | Alignment of a table column.
  type Alignment = 
    | AlignLeft
    | AlignRight
    | AlignCenter
    | AlignDefault

  //-- | Inline elements.
  type Inline = 
    | Str of string                     //-- ^ Text (string)
    | Emph of Inline list               //-- ^ Emphasized text (list of inlines)
    | Strong of Inline list             //-- ^ Strongly emphasized text (list of inlines)
    | Strikeout of Inline list          //-- ^ Strikeout text (list of inlines)
    | Superscript of Inline list        //-- ^ Superscripted text (list of inlines)
    | Subscript of Inline list          //-- ^ Subscripted text (list of inlines)
    | SmallCaps of Inline list          //-- ^ Small caps text (list of inlines)
    | Quoted of QuoteType * Inline list     //-- ^ Quoted text (list of inlines)
    | Cite of Citation list * Inline list   //-- ^ Citation (list of inlines)
    | Code of Attr * string             //-- ^ Inline code (literal)
    | Space                             //-- ^ Inter-word space
    | LineBreak                         //-- ^ Hard line break
    | Math of MathType * string         //-- ^ TeX math (literal)
    | RawInline of Format * string      //-- ^ Raw inline
    | Link of Inline list * Target      //-- ^ Hyperlink: text (list of inlines), target
    | Image of Inline list * Target     //-- ^ Image:  alt text (list of inlines), target
    | Note of Block list                //-- ^ Footnote or endnote

  and CitationMode = AuthorInText | SuppressAuthor | NormalCitation
  and Citation = { 
    citationId: string;
    citationPrefix: Inline list; 
    citationSuffix: Inline list; 
    citationMode: CitationMode; 
    citationNoteNum: int; 
    citationHash: int
  }

  //-- | Block element.
  and Block = 
    | Plain of Inline list              //-- ^ Plain text, not a paragraph
    | Para of Inline list               //-- ^ Paragraph
    | CodeBlock of Attr * string        //-- ^ Code block (literal) with attributes
    | RawBlock of Format * string       //-- ^ Raw block
    | BlockQuote of Block list          //-- ^ Block quote (list of blocks)
    | OrderedList of ListAttributes * Block list list   //-- ^ Ordered list (attributes
                                                        //-- and a list of items, each a list of blocks)
    | BulletList of Block list list     //-- ^ Bullet list (list of items, each
                                        //-- a list of blocks)
    | DefinitionList of list<Inline list * Block list list>   //-- ^ Definition list
                                                              //-- Each list item is a pair consisting of a
                                                              //-- term (a list of inlines) and one or more
                                                              //-- definitions (each a list of blocks)
    | Header of int * Attr * list<Inline>  //-- ^ Header - level (integer) and text (inlines)
    | HorizontalRule                    //-- ^ Horizontal rule
    | Table of list<Inline> * list<Alignment> * list<double> * list<TableCell> * list<list<TableCell>>   //-- ^ Table,
                                                  //-- with caption, column alignments,
                                                  //-- relative column widths (0 = default),
                                                  //-- column headers (each a list of blocks), and
                                                  //-- rows (each a list of lists of blocks)
    | Null                              //-- ^ Nothing

  //-- | Table cells are list of Blocks
  and TableCell = list<Block>

  type SharpDoc = list<Block>