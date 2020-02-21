use std::env;
use std::error::Error;
use std::fs::File;
use std::io::prelude::*;
use std::path::Path;

mod parser;

fn main() {
    let args: Vec<String> = env::args().collect();
    if args.len() != 2 {
        println!("Please provide Ganit program file as an argument");
    } else {
        let file_name = args[1].to_string();

        let path = Path::new(&file_name);
        let display = path.display();

        let mut program = String::new();

        let _file = match File::open(&path) {
            // The `description` method of `io::Error` returns a string that
            // describes the error
            Err(why) => println!("Couldn't open `{}`: {}", display,
                                 why.description()),
            Ok(mut file) => {
                match file.read_to_string(&mut program) {
                    Err(why) => println!("Couldn't read {}: {}", display, why.description()),
                    Ok(_) => {
                        process_program(program);
                    }
                }
            }
        };
    }
}

fn process_program(program: String) {
    let tokens = parser::tokenize(program);
    for token in tokens.iter() {
        println!("{}", token)
    }
}