set nocompatible              " be iMproved, required
filetype off                  " required

" set the runtime path to include Vundle and initialize
set rtp+=~/.vim/bundle/Vundle.vim
call vundle#begin()
" alternatively, pass a path where Vundle should install plugins
"call vundle#begin('~/some/path/here')

" let Vundle manage Vundle, required
Plugin 'VundleVim/Vundle.vim'

" The following are examples of different formats supported.
" Keep Plugin commands between vundle#begin/end.

"(1) plugin on GitHub repo
	"about Git
Plugin 'tpope/vim-fugitive'
Plugin 'scrooloose/nerdtree'
Plugin 'tomasr/molokai'
Plugin 'Solarized'
Plugin 'vim-airline/vim-airline'
Plugin 'vim-airline/vim-airline-themes'
Plugin 'scrooloose/nerdcommenter'
Plugin 'AutoComplPop'
Bundle 'fholgado/minibufexpl.vim'
Plugin 'vim-syntastic/syntastic'
Plugin 'kien/ctrlp.vim'

if version > 750
Plugin 'Valloric/YouCompleteMe'
endif
Plugin 'DoxygenToolkit.vim'
Plugin 'marijnh/tern_for_vim'
"(2) plugin from http://vim-scripts.org/vim/scripts.html
Plugin 'L9'

"(3) Git plugin not hosted on GitHub
	"fast locate file
Plugin 'git://git.wincent.com/command-t.git'
	
Plugin 'git://github.com/jiangmiao/auto-pairs.git'

"(4) git repos on your local machine (i.e. when working on your own plugin)
"Plugin 'file:///home/gmarik/path/to/plugin'
" The sparkup vim script is in a subdirectory of this repo called vim.
" Pass the path to set the runtimepath properly.
	"about HTML 
Plugin 'rstacruz/sparkup', {'rtp': 'vim/'}


" All of your Plugins must be added before the following line
call vundle#end()            " required
filetype plugin indent on    " required
" To ignore plugin indent changes, instead use:
filetype plugin on
"
" Brief help
" :PluginList       - lists configured plugins
" :PluginInstall    - installs plugins; append `!` to update or just :PluginUpdate
" :PluginSearch foo - searches for foo; append `!` to refresh local cache
" :PluginClean      - confirms removal of unused plugins; append `!` to auto-approve removal
"
" see :h vundle for more details or wiki for FAQ
" Put your non-Plugin stuff after this line

"------------------------------------------------------------------bundle
"===============================nerdTree start 
let NERDTreeChDirMode=2
let NERDTreeWinPos='right'
let NERDTreeWinSize=25
map <C-n> :NERDTreeToggle<CR>
autocmd StdinReadPre * let s:std_in=1
autocmd VimEnter * if argc() == 1 && isdirectory(argv()[0]) && !exists("s:std_in") | exe 'NERDTree' argv()[0] | wincmd p | ene | endif
let g:NERDTreeDirArrowExpandable = '+'
let g:NERDTreeDirArrowCollapsible = '~'
let NERDTreeIgnore=['\.meta$', '\.d.ts', '\~$', '\.swp$']

" NERDTress File highlighting
function! NERDTreeHighlightFile(extension, fg, bg, guifg, guibg)
 exec 'autocmd filetype nerdtree highlight ' . a:extension .' ctermbg='. a:bg .' ctermfg='. a:fg .' guibg='. a:guibg .' guifg='. a:guifg
 exec 'autocmd filetype nerdtree syn match ' . a:extension .' #^\s\+.*'. a:extension .'$#'
endfunction

"call NERDTreeHighlightFile('js', 'Red', 'none', '#ffa500', '#151515')
"=============================== nerdTree end

"==============================nerdcommenter
" :map  see command
	" Add spaces after comment delimiters by default
let g:NERDSpaceDelims = 1
" Use compact syntax for prettified multi-line comments
let g:NERDCompactSexyComs = 1
	" Align line-wise comment delimiters flush left instead of following code indentation
let g:NERDDefaultAlign = 'left'
	" Set a language to use its alternate delimiters by default
let g:NERDAltDelims_java = 1
	" Add your own custom formats or override the defaults
let g:NERDCustomDelimiters = { 'c': { 'left': '/**','right': '*/' }}
	" Allow commenting and inverting empty lines (useful when commenting a region)
let g:NERDCommentEmptyLines = 1
	" Enable trimming of trailing whitespace when uncommenting
let g:NERDTrimTrailingWhitespace = 1
"==============================nerdcommenter
"
"===============================syntastic start
let g:syntastic_error_symbol='✗'
let g:syntastic_warning_symbol='⚠'
" let g:syntastic_warning_symbol='!'
" let g:syntastic_always_populate_loc_list = 1
" let g:syntastic_auto_loc_list =1
let g:syntastic_check_on_open=1
let g:syntastic_enable_signs=1
" let g:syntastic_check_on_wq=1
let g:syntastic_enable_highlighting=1
"let g:syntastic_python_checkers=['pyflakes'] " 浣跨敤pyflakes,閫熷害姣攑ylint蹇?
let g:syntastic_javascript_checkers = ['jshint'] "npm install jshint -g  对js 需要安装jshint
"let g:syntastic_html_checkers=['tidy', 'jshint']
" 淇敼楂樹寒鐨勮儗鏅壊, 閫傚簲涓婚
" highlight SyntasticErrorSign guifg=red guibg=black
highlight SyntasticWarningSign guifg=white guibg=red

"============================== syntastic  end

"==============================airline  start
let g:airline#extensions#tabline#enabled = 0
let g:airline#extensions#tabline#left_sep = ' '
let g:airline#extensions#tabline#left_alt_sep = '|'
" let g:airline_theme='base16_default' "++++
" let g:airline_theme='base16_embers' "+++
let g:airline_theme='gruvbox'
let g:airline_powerline_fonts=1
"==============================airline  end
"
"==============================minibufexpl  end
"let g:miniBufExplMapWindowNavVim = 1   
let g:miniBufExplMapWindowNavArrows = 1   
let g:miniBufExplMapCTabSwitchBufs = 1   
let g:miniBufExplModSelTarget = 1  
let g:miniBufExplMoreThanOne=0
" 左右minibuff的切换设置
map <F11> :MBEbp<CR> 
map <F12> :MBEbn<CR>
"==============================minibufexpl  end


"==============================DoxygenToolkit start
"map <C-i> : Dox<cr>
"let g:DoxygenToolkit_briefTag_pre="@Synopsis  " 
" let g:DoxygenToolkit_paramTag_pre='@param '
" let g:DoxygenToolkit_returnTag="@returns "
" let g:DoxygenToolkit_briefTag_pre ="@brief "
" let g:DoxygenToolkit_undocTag="DOXIGEN_SKIP_BLOCK"
let g:DoxygenToolkit_briefTag_funcName="yes"
" let g:DoxygenToolkit_maxFunctionProtoLines = 30
"let g:DoxygenToolkit_blockHeader="--------------------------------------------------------------------------" 
"let g:DoxygenToolkit_blockFooter="----------------------------------------------------------------------------" 
let g:DoxygenToolkit_authorName="wangff, 2422312148@qq.com" 
"let g:DoxygenToolkit_licenseTag="My own license"   <-- !!! Does not end with "\<enter>"
"============================== DoxygenToolkit end

"============================== YouCompleteMe start
if version > 750
let g:ycm_global_ycm_extra_conf = '~/.vim/bundle/YouCompleteMe/third_party/ycmd/cpp/ycm/.ycm_extra_conf.py'
" 设置跳转到方法/函数定义的快捷键 
nnoremap <leader>j :YcmCompleter GoToDefinitionElseDeclaration<CR> 
" 触发补全快捷键 
" let g:ycm_key_list_select_completion = ['<TAB>', '<c-n>', '<Down>']"
" let g:ycm_key_list_previous_completion = ['<S-TAB>', '<c-p>', '<Up>']
let g:ycm_auto_trigger = 1 
" 最小自动触发补全的字符大小设置为 3 
let g:ycm_min_num_of_chars_for_completion = 3 
" YCM的previw窗口比较恼人，还是关闭比较好 
set completeopt-=preview
endif
"============================== YouCompleteMe end

"============================== ag start
set runtimepath^=~/.vim/bundle/ag
let g:ag_working_path_mode='r'
let g:ag_prg="ag --vimgrep"
" let g:ag_highlight=1
"============================== ag end
"------------------------------------------------------------------bundle


"----------------------------------------costumize 
set runtimepath^=~/.vim/colors
syntax on
set ai
set ci
set noet
set sw=4
set encoding=utf-8
set backspace=indent,eol,start
let &termencoding=&encoding
set fileencodings=utf-8,gbk
set tabstop=4
set shiftwidth=4
set number
filetype on
set autoindent
set smartindent
set showmatch
set incsearch
set t_Co=256

if has('gui_running')
	set background=dark
	" let g:molokai_original=1
	" let g:rehash256=1
	" colorscheme molokai
	" colorscheme wombat256mod
	" colorscheme solarized
	colorscheme gruvbox
	"set guifont=Consolas:h19
	" set guifont=Menlo_Regular:h18
 	set guifont=DejaVu_Sans_Mono_for_Powerline:h16
	
	 
	set cursorline
	" au InsertEnter * hi Cursor guibg=yellow
	" au InsertLeave * hi Cursor guibg=gray
else
	set background=dark
	colorscheme desert
endif

set path=.,~/Desktop/work/kingdom/client/kingdom/assets/script/**
set path+=~/Desktop/work/kingdom/client/kingdom/assets/resources/i18n/**

set path+=~/Desktop/work/kingdom/client/kingdom/library
set path+=~/Desktop/work/kingdom/client-build/jsb-default/src



set linespace=4

"split navigations
nnoremap <C-J> <C-W><C-J>
nnoremap <C-K> <C-W><C-K>
nnoremap <C-L> <C-W><C-L>
nnoremap <C-H> <C-W><C-H>


" js格式化
command! JsonFormat :execute '%!python -m json.tool'
  \ | :execute '%!python -c "import re,sys;chr=__builtins__.__dict__.get(\"unichr\", chr);sys.stdout.write(re.sub(r\"\\u[0-9a-f]{4}\", lambda x: chr(int(\"0x\" + x.group(0)[2:], 16)), sys.stdin.read()))"'
  \ | :%s/ \+$//ge
  \ | :set ft=javascript
  \ | :1





