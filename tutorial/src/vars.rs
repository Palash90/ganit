pub fn run() {
    let name = "Palash";
    let mut age = 29; // This will be warned as this value is never read.
    age = 30;
    println!("My name is {}, my age is {}", name, age);

    // Defining constants, needed explicit type definition
    const ONE: i32 = 1;
    println!("{}", ONE);

    // Assigning multiple variables at once
    let (my_name, my_age) = ("Palash", 29);
    println!("{} is {}", my_name, my_age);

    // Print tuple
    println!("{:?}", (my_name, my_age));

    let (my_name, my_age) = ("Palash", 29);
    println!("{:?}", (my_name, my_age));

    let age=30;
    println!("My age is {}", age);
}