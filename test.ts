class Person {
    age: string = null;

    init(): void{
        console.log('d');
    }

}

class Student extends Person {
    init(){
        super.init();
        let b = 2;
        var a = "d";
        console.log('student');
    }
}

var s = new Student();
s.init();
