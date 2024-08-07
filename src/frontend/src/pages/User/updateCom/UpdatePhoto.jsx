
const UpdatePhoto = () => {
  return (
    <div className="p-3 border-t mt-3">
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
  );
};

export default UpdatePhoto;
