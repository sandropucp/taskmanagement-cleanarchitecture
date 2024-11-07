# Invarients (Business Rules)

## Task Invariants

- A Task must have a Name, an Assignee, Status, Priority and Due date.

- Due dates must be realistic for scheduling.
Invariant: Task.DueDate >= currentDate at the time of setting.

- Controls the assignment flow.
Invariant: AssignTask() operation is allowed only if User.Role == Admin or User == Task.Owner.

## Comment Invariants

- A Comment must be associated with a Task. All comments relate to specific Tasks for context.
Invariant: Comment.TaskId cannot be null.

- A Comment can only be edited by its author or an Admin
Invariant: EditComment() is allowed if User == Comment.Author or User.Role == Admin.

## Category Invariants

- Categories must be unique by name
Rationale: Prevents duplicate categories that could confuse users.
Invariant: Category.Name is unique within the system.

## Role and Permission Invariants

- Only Admins can delete Tasks or Comments
Rationale: Prevents unauthorized deletions.
Invariant: DeleteTask() and DeleteComment() can be executed only if User.Role == Admin.

- Only Admins can assign User roles
Rationale: Maintains control over role management.
Invariant: AssignRole(User, Role) is allowed only if User.Role == Admin.

## General Invariants

- Timestamps must always be in the past or present
Rationale: Ensures accurate tracking of task and comment events.
Invariant: All Timestamp values, such as Task.CreatedAt and Comment.Timestamp, must be <= currentDateTime.

- Completed Tasks cannot be reassigned
Rationale: Once completed, a Task should be static.
Invariant: Task.Assignee cannot be modified if Task.Status == Completed.
