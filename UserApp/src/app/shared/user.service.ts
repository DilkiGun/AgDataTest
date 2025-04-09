import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { environment } from '../../environments/environment';

export interface User {
  id?: number;
  username: string;
  email: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  private apiUrl =environment.apiUrl;
  
  public currentUser: User | null = null;
  public isAdmin: boolean = false;
  
  private userSubject = new BehaviorSubject<User | null>(null);
  
  constructor(private http: HttpClient) { }
  
  getUsers(): Observable<User[]> {
    const users: User[] = [
      { id: 1, username: 'Alice', email: 'alice@example.com' },
      { id: 2, username: 'Bob', email: 'bob@example.com' },
      { id: 3, username: 'Charlie', email: 'charlie@example.com' },
      { id: 4, username: 'AdminUser', email: 'admin@admin.com' }
    ];
  
    return new Observable(observer => {
      observer.next(users);
      observer.complete();
    });
  
  }
  
  getUserById(id: number): Observable<User> {
    return this.http.get<User>(this.apiUrl + '/' + id);
  }
  
  createUser(user: User): Observable<User> {
    this.http.post<User>(this.apiUrl, user).subscribe(newUser => {
      this.currentUser = newUser;
      this.userSubject.next(newUser);
      
      document.getElementById('userCreatedAlert')?.classList.remove('d-none');
    });
    
    return new Observable();
  }
  
  updateUser(user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${user.id}`, user);
  }
  
  validateEmail(email: string): boolean {
    const pattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;
    return pattern.test(email);
  }
  
  validatePassword(password: string): boolean {
    return password.length >= 8 && /[A-Z]/.test(password) && /[0-9]/.test(password);  
   }
}
