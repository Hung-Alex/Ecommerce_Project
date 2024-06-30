import React, { useState } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import './Login.css'

const AuthModals = () => {
  const [showLogin, setShowLogin] = useState(false);
  const [showRegister, setShowRegister] = useState(false);

  const handleLoginClose = () => setShowLogin(false);
  const handleLoginShow = () => setShowLogin(true);
  const handleRegisterClose = () => setShowRegister(false);
  const handleRegisterShow = () => {
    setShowLogin(false);
    setShowRegister(true);
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
          <Form>
            <Form.Group controlId="formBasicEmail">
              <Form.Label>Email address</Form.Label>
              <Form.Control type="email" placeholder="Enter email" />
            </Form.Group>

            <Form.Group controlId="formBasicPassword">
              <Form.Label>Password</Form.Label>
              <Form.Control type="password" placeholder="Password" />
            </Form.Group>

            <Button variant="primary" type="submit">
              Login
            </Button>
          </Form>
          <div className="mt-3 text-center">
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
          <Form>
            <Form.Group controlId="formBasicEmail">
              <Form.Label>Email address</Form.Label>
              <Form.Control type="email" placeholder="Enter email" />
            </Form.Group>

            <Form.Group controlId="formBasicPassword">
              <Form.Label>Password</Form.Label>
              <Form.Control type="password" placeholder="Password" />
            </Form.Group>

            <Form.Group controlId="formBasicConfirmPassword">
              <Form.Label>Confirm Password</Form.Label>
              <Form.Control type="password" placeholder="Confirm Password" />
            </Form.Group>

            <Button variant="primary" type="submit">
              Register
            </Button>
          </Form>
        </Modal.Body>
      </Modal>
    </>
  );
};

export default AuthModals;
