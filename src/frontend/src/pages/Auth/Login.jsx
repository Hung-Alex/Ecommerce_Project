import { Link, useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-hot-toast";
import { useForm } from "react-hook-form";
import { GoogleOAuthProvider } from "@react-oauth/google";
import { useContext, useState } from "react";
import { UserContext } from "../../context/UserContext";
import axios from "../../utils/axios";
import GoogleLoginButton from "./GoogleLoginButton";
import Header from "../../components/Shared/Header/Header";
import Footer from "../../components/Shared/Footer/Footer";

function Login() {
  const [loading, setLoading] = useState(false);
  const { login } = useContext(UserContext);
  const { register, handleSubmit } = useForm();
  const location = useLocation();
  const navigate = useNavigate();
  const from = location.state?.from?.pathname || "/";

  const onSubmit = async (data) => {
    setLoading(true);
    try {
      const res = await axios.post("/authentications/login", data);
      if (res.data) {
        toast.success("Login successfully");
        localStorage.setItem("accessToken", res.data.data.accessToken);
        localStorage.setItem("refreshToken", res.data.data.refreshToken);
        login(res.data.data);
        navigate(from, { replace: true });
      }
    } catch (error) {
      toast.error("Invalid username or password");
      console.error("Login error:", error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <GoogleOAuthProvider clientId="57958274400-e0226k4bdrb3b5bp3hrq4fp1p9rebeo6.apps.googleusercontent.com">
      <section className="flex justify-center pt-12 mb-12">
        <div className="border p-4 rounded shadow-md">
          <div className="mb-8 my-4">
            <h2 className="text-3xl font-bold">
              Welcome to <span className="text-[#274C5B]">Organic Food</span>
            </h2>
            <p className="text-sm text-[#7EB693] mt-2">
              Choose the best healthier way of life
            </p>
          </div>
          <h1 className="text-2xl font-bold">Login</h1>
          <form
            onSubmit={handleSubmit(onSubmit)}
            className="text-[#9796A1] w-[400px] flex flex-col justify-around mt-3"
            autoComplete="on"
          >
            <div className="my-2">
              <label className="text-left" htmlFor="username">
                Username
              </label>
              <br />
              <input
                {...register("username", { required: true })}
                name="username"
                className="focus:ring-1 focus:outline-none focus:ring-[#274C5B] w-full h-12 rounded-lg pl-3 text-black border mt-2"
                type="text"
                placeholder="try user or admin"
                autoComplete="username"
              />
            </div>
            <div className="my-2">
              <label className="text-left" htmlFor="password">
                Password
              </label>
              <br />
              <input
                {...register("password", { required: true })}
                name="password"
                className="focus:ring-1 focus:outline-none focus:ring-[#274C5B] w-full h-12 rounded-lg pl-3 text-black border mt-2"
                type="password"
                placeholder="Password"
                autoComplete="current-password"
              />
            </div>
            <Link className="text-[#274C5B]" to="/reset-password">
              Forgot password?
            </Link>
            <div className="flex justify-center mt-8">
              <input
                className="uppercase bg-[#274C5B] hover:bg-[#326072] font-medium text-white rounded-full w-1/2 py-[10px] cursor-pointer transition-all duration-500 ease-in-out"
                type="submit"
                value={`${loading ? "Loading..." : "Login"}`}
                disabled={loading}
              />
            </div>
          </form>
          <GoogleLoginButton />
          <p className="my-4 text-center">
            Don't have an account?{" "}
            <Link className="text-[#7EB693]" to="/signup">
              Sign Up
            </Link>
          </p>
        </div>
      </section>
    </GoogleOAuthProvider>
  );
}

export default Login;
