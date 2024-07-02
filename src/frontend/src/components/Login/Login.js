import React, { useState } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import { registerUser, loginUser } from '../../services/authService';
import './Login.css';

const AuthModals = () => {
  const [showLogin, setShowLogin] = useState(false);
  const [showRegister, setShowRegister] = useState(false);
  const [userName, setUserName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);

  const handleLoginClose = () => setShowLogin(false);
  const handleLoginShow = () => setShowLogin(true);
  const handleRegisterClose = () => setShowRegister(false);
  const handleRegisterShow = () => {
    setShowLogin(false);
    setShowRegister(true);
  };

  const handleRegisterSubmit = async (event) => {
    event.preventDefault();
    if (password !== confirmPassword) {
      setError('Passwords do not match.');
      setSuccess(null);
      return;
    }

    try {
      const userData = { userName, email, password, confirmPassword };
      const registerResponse = await registerUser(userData);
      setSuccess('User registered successfully!');
      setError(null);
      console.log('Register response:', registerResponse);

      // Đăng nhập tự động sau khi đăng ký thành công
      const loginData = { userName, password };
      const loginResponse = await loginUser(loginData);
      console.log('Login response:', loginResponse);
      setShowRegister(false);

    } catch (error) {
      setError(`Registration or login failed: ${error.response ? error.response.data : error.message}`);
      setSuccess(null);
    }
  };

  const handleLoginSubmit = async (event) => {
    event.preventDefault();

    try {
      const loginData = { userName, password };
      const loginResponse = await loginUser(loginData);
      console.log('Login response:', loginResponse);
      setShowLogin(false);

    } catch (error) {
      setError(`Login failed: ${error.response ? error.response.data : error.message}`);
      setSuccess(null);
    }
  };

  return (
    <>
      <p variant="outline-light" onClick={handleLoginShow}>
        Login
      </p>

      <Modal show={showLogin} onHide={handleLoginClose} centered>
        <Modal.Header closeButton className="modal-header">
          <Modal.Title className="modal-title">Login</Modal.Title>
        </Modal.Header>
        <Modal.Body className="modal-body">
          <Form onSubmit={handleLoginSubmit}>
            <Form.Group controlId="formBasicUserName">
              <Form.Label>Username</Form.Label>
              <Form.Control 
                type="text" 
                placeholder="Enter username" 
                value={userName} 
                onChange={(e) => setUserName(e.target.value)} 
                required 
              />
            </Form.Group>

            <Form.Group controlId="formBasicPassword">
              <Form.Label>Password</Form.Label>
              <Form.Control 
                type="password" 
                placeholder="Password" 
                value={password} 
                onChange={(e) => setPassword(e.target.value)} 
                required 
              />
            </Form.Group>

            <Button variant="primary" type="submit">
              Login
            </Button>
          </Form>
          <div className="mt-3 text-center">
            {error && <p className="text-danger">{error}</p>}
            {success && <p className="text-success">{success}</p>}
            <p>
              Don't have an account?{' '}
              <Button variant="link" onClick={handleRegisterShow} className="btn-link">
                Register
              </Button>
            </p>
          </div>
        </Modal.Body>
      </Modal>

      <Modal show={showRegister} onHide={handleRegisterClose} centered>
        <Modal.Header closeButton className="modal-header">
          <Modal.Title className="modal-title">Register</Modal.Title>
        </Modal.Header>
        <Modal.Body className="modal-body">
          <Form onSubmit={handleRegisterSubmit}>
            <Form.Group controlId="formBasicUserName">
              <Form.Label>Username</Form.Label>
              <Form.Control 
                type="text" 
                placeholder="Enter username" 
                value={userName} 
                onChange={(e) => setUserName(e.target.value)} 
                required 
              />
            </Form.Group>

            <Form.Group controlId="formBasicEmail">
              <Form.Label>Email address</Form.Label>
              <Form.Control 
                type="email" 
                placeholder="Enter email" 
                value={email} 
                onChange={(e) => setEmail(e.target.value)} 
                required 
              />
            </Form.Group>

            <Form.Group controlId="formBasicPassword">
              <Form.Label>Password</Form.Label>
              <Form.Control 
                type="password" 
                placeholder="Password" 
                value={password} 
                onChange={(e) => setPassword(e.target.value)} 
                required 
              />
            </Form.Group>

            <Form.Group controlId="formBasicConfirmPassword">
              <Form.Label>Confirm Password</Form.Label>
              <Form.Control 
                type="password" 
                placeholder="Confirm Password" 
                value={confirmPassword} 
                onChange={(e) => setConfirmPassword(e.target.value)} 
                required 
              />
            </Form.Group>

            <Button variant="primary" type="submit">
              Register
            </Button>
          </Form>
          <div className="mt-3 text-center">
            {error && <p className="text-danger">{error}</p>}
            {success && <p className="text-success">{success}</p>}
          </div>
        </Modal.Body>
      </Modal>
    </>
  );
};

export default AuthModals;
