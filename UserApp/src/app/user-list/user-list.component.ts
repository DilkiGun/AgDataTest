import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService, User } from '../shared/user.service';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  imports: [CommonModule,ReactiveFormsModule],
})
export class UserListComponent implements OnInit, OnDestroy {
  users: User[] = [];
  filteredUsers: User[] = [];
  selectedUser: User | null = null;
  userForm: FormGroup;
  private subscriptions: Subscription[] = [];

  isLoading: boolean = true;

  constructor(private userService: UserService, private fb: FormBuilder) {

    this.userForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
    const usersSub = this.userService.getUsers().subscribe({
      next: data => {
        this.users = data;
        this.filteredUsers = data;
        this.isLoading = false;
      },
      error: err => console.error('Error fetching users:', err)
    });

    this.subscriptions.push(usersSub);
  }

  filterUsers(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement) {
      const searchTerm = inputElement.value.toLowerCase();
      this.filteredUsers = this.users.filter(user => 
        user.username.toLowerCase().includes(searchTerm) || 
        user.email.toLowerCase().includes(searchTerm)
      );
    }
  }

  createUser(): void {
    if (this.userForm.invalid) return;

    const newUser: User = this.userForm.value;

    const createSub = this.userService.createUser(newUser).subscribe({
      next: user => {
        this.users.push(user);
        this.filteredUsers = [...this.users];
        this.userForm.reset();
      },
      error: err => console.error('Error creating user:', err)
    });

    this.subscriptions.push(createSub);
  }

  trackById(index: number, item: User): number {
    return item.id!;
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }
}
