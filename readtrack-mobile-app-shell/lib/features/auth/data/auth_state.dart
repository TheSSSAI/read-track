import 'package:equatable/equatable.dart';
import 'package:readtrack_apiclient/readtrack_apiclient.dart';
import 'auth_status.dart';

/// Represents the immutable state of the authentication feature.
///
/// This class encapsulates the current authentication status, the authenticated
/// user's profile (if applicable), and any error messages resulting from
/// authentication attempts. It uses [Equatable] to ensure value equality,
/// minimizing unnecessary UI rebuilds in Riverpod.
class AuthState extends Equatable {
  /// The current status of the authentication session.
  final AuthStatus status;

  /// The profile of the authenticated user.
  ///
  /// This value is `null` if [status] is [AuthStatus.unauthenticated] or [AuthStatus.initial].
  final UserProfileDto? user;

  /// An optional error message describing why an authentication action failed.
  ///
  /// This is typically populated when [status] is [AuthStatus.failure].
  final String? errorMessage;

  /// Creates a constant [AuthState].
  const AuthState({
    this.status = AuthStatus.initial,
    this.user,
    this.errorMessage,
  });

  /// Factory constructor for the initial state of the application.
  ///
  /// Represents the state before any authentication check has occurred.
  factory AuthState.initial() {
    return const AuthState(status: AuthStatus.initial);
  }

  /// Factory constructor for an authenticated state.
  ///
  /// @param user The profile of the successfully authenticated user.
  factory AuthState.authenticated(UserProfileDto user) {
    return AuthState(
      status: AuthStatus.authenticated,
      user: user,
      errorMessage: null,
    );
  }

  /// Factory constructor for an unauthenticated state.
  ///
  /// Represents a user who is not logged in or has explicitly logged out.
  factory AuthState.unauthenticated() {
    return const AuthState(
      status: AuthStatus.unauthenticated,
      user: null,
      errorMessage: null,
    );
  }

  /// Factory constructor for a failure state.
  ///
  /// @param message A description of the error.
  factory AuthState.failure(String message) {
    return AuthState(
      status: AuthStatus.failure,
      user: null,
      errorMessage: message,
    );
  }

  /// Creates a copy of this [AuthState] but with the given fields replaced with the new values.
  AuthState copyWith({
    AuthStatus? status,
    UserProfileDto? user,
    String? errorMessage,
  }) {
    return AuthState(
      status: status ?? this.status,
      user: user ?? this.user,
      errorMessage: errorMessage ?? this.errorMessage,
    );
  }

  /// Helper getter to determine if the state represents an authenticated user.
  bool get isAuthenticated => status == AuthStatus.authenticated;

  /// Helper getter to determine if the state represents an unauthenticated user.
  bool get isUnauthenticated => status == AuthStatus.unauthenticated;

  /// Helper getter to determine if the auth status is currently being determined (initial).
  bool get isInitial => status == AuthStatus.initial;

  /// Helper getter to determine if the state represents an error condition.
  bool get hasError => status == AuthStatus.failure && errorMessage != null;

  @override
  List<Object?> get props => [status, user, errorMessage];

  @override
  String toString() {
    return 'AuthState(status: $status, user: ${user?.id}, errorMessage: $errorMessage)';
  }
}