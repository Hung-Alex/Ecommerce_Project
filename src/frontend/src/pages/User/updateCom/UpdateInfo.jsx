import { useContext, useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "react-hot-toast";
import axios from "../../../utils/axios";
import UpdatePhoto from "./UpdatePhoto";
import UpdatePassword from "./UpdatePassword";
import { useNavigate } from "react-router-dom";
import { UserContext } from "../../../context/UserContext";

const UpdateInformation = () => {
  // const { user, setUser } = useContext(UserContext);
  const user = {
    name: 'Tuan',
    email: 'tuan@example.com',
    address: {
      city: 'HCMC',
      street: '123 Nguyen Hue'
    },
    phone: '123-456-7890',
    image: {
      secure_url: '' // replace with actual image URL if available
    }
  };
  const [loading, setLoading] = useState(false);
  const { register, handleSubmit } = useForm();
  const navigate = useNavigate();

  const onSubmit = async (data) => {
    const updateInfo = {
      name: data.name,
      email: user?.email,
      phone: data.phone,
      address: {
        city: data.city,
        street: data.street,
        postalCode: data.postalCode,
      },
    };

    try {
      setLoading(true);
      const res = await axios.patch("/user/update", updateInfo, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });
      setLoading(false);
      if (res.status === 200) {
        const notify = () => toast.success("Information updated");
        setUser(res.data.data);
        navigate("/user/profile");
        notify();
      }
    } catch (error) {
      setLoading(false);
      const notify = () => toast.error("Information not updated");
      notify();
    }
  };

  return (
    <div className=" max-h-[85vh] overflow-auto border-t-2 border-[#7eb693] shadow-md rounded bg-white max-w-[70vw]">
      <h3 className="text-xl p-3">Personal Information</h3>
      

        {/* image section */}
        <div className="p-3  border-t mt-3">
          <form>
            <label htmlFor="photo">Your Profile Photo</label>
            <img
              className="mt-2 rounded-full bg-slate-200 w-32 h-32 object-cover"
              src=""
              alt="photo"
            />
            <div className="mt-4">
              <input
                className="bg-[#7EB693] ml-4 px-4 py-1 rounded text-white cursor-pointer"
                type="submit"
                value="Save"
              />
            </div>
          </form>
        </div>

        {/* User section */}
        <div>
          <div className="p-3 ">
            <span className="mb-6 block">
              <label className="block mb-2" htmlFor="name">
                Name
              </label>
              <input
                className="border bg-slate-50 w-1/2 outline-none p-3 rounded-md text-sm"
                type="text"
                id="name"
                defaultValue={user.name}
                {...register("name", { required: true })}
              />
            </span>
            <div className="border-t pt-2">
              <span>
                <label
                  className="block mb-2 text-xl border-b pb-2"
                  htmlFor="address"
                >
                  Address
                </label>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-3 border-b pb-6">
                  <span>
                    <label htmlFor="city">City</label>
                    <input
                      className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                      type="text"
                      id="city"
                      defaultValue={user?.address.city}
                      {...register("city")}
                    />
                  </span>
                  <span>
                    <label htmlFor="street">Street</label>
                    <input
                      className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                      type="text"
                      id="street"
                      defaultValue={user?.address.street}
                      {...register("street")}
                    />
                  </span>
                  <span>
                    <label htmlFor="postalCode">Postal Code</label>
                    <input
                      className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
                      type="text"
                      id="postalCode"
                      defaultValue={user?.address.postalCode}
                      {...register("postalCode")}
                    />
                  </span>
                </div>
              </span>
            </div>
            <span>
              <label className="block mt-6 mb-2" htmlFor="phone">
                Mobile number
              </label>
              <input
                className="border bg-slate-50 w-1/2 outline-none p-3 rounded-md text-sm"
                type="text"
                id="phone"
                defaultValue={user.phone}
                {...register("phone")}
              />
            </span>
          </div>
        </div>
        <div className="px-4 mt-4">
          <input
            className="bg-[#7EB693]  px-6 py-2 rounded text-white cursor-pointer"
            type="submit"
            value={`${loading ? "Updating..." : "Update"}`}
            disabled={loading}
          />
        </div>

        <div className="border-t mt-3 pb-4">
      <h3 className="text-xl border-b p-3">Change Password</h3>
      <form>
        <div className="p-3 grid grid-cols-1 md:grid-cols-2 gap-3">
          <span>
            <label className="block mb-2" htmlFor="password">
              Old password
            </label>
            <input
              className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm mb-4"
              type="password"
             
            />
          </span>
          <span>
            <label className="block mb-2" htmlFor="password">
              New password
            </label>
            <input
              className="border bg-slate-50 w-full outline-none p-3 rounded-md text-sm"
              type="password"
              {...register("newPassword")}
            />
          </span>
          <div>
            <input
              className="bg-[#7EB693]  px-4 py-2 rounded text-white cursor-pointer"
              type="submit"
              value="Save"
            />
          </div>
        </div>
      </form>
    </div>
    </div>
  );
};

export default UpdateInformation;
