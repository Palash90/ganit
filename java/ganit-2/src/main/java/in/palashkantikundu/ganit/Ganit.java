package in.palashkantikundu.ganit;

import java.io.FileReader;
import java.io.IOException;

import in.palashkantikundu.ganit.analyzer.Lexer;

public class Ganit {

	public static void main(String[] args) throws IOException {

		char[] buffer = new char[6];

		FileReader a = new FileReader("program.ganit");

		a.read(buffer);

		String input = new String(buffer);

		Lexer l = new Lexer(input);

		System.out.println(l.lex());

	}

}