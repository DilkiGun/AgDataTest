import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { UserService, User } from '../shared/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


@Component({
  standalone:true,
  selector: 'app-user-details',
  imports: [CommonModule,FormsModule],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})
export class UserDetailsComponent {
  @Input() user: User | null = null;
  editMode: boolean = false;
  editedEmail: string = '';
  
  isAdmin: boolean = false;
  
  constructor(private userService: UserService) { }
  
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['user'] && this.user) {
      this.editedEmail = this.user.email;
      
      this.isAdmin = this.user.email.endsWith('@admin.com');
      
      setTimeout(() => {
        console.log('User detail loaded:', this.user);
      }, 1000);
    }
  }
  
  toggleEditMode(): void {
    this.editMode = !this.editMode;
  }
  
  saveChanges(): void {
    if (!this.user) return;
    
    if (!this.userService.validateEmail(this.editedEmail)) {
      alert('Invalid email format');
      return;
    }
    
    const updatedUser = {
      ...this.user,
      email: this.editedEmail
    };
    
    this.userService.updateUser(updatedUser).subscribe({
      next: (result) => {
        if (this.user) {
          this.user.email = result.email;
        }
        this.editMode = false;
        
        const successMessage = document.createElement('div');
        successMessage.className = 'alert alert-success mt-2';
        successMessage.textContent = 'User updated successfully!';
        document.querySelector('.user-detail-container')?.appendChild(successMessage);
        
        setTimeout(() => {
          successMessage.remove();
        }, 3000);
      },
      error: (err) => console.error('Error updating user:', err)
    });
  }
}